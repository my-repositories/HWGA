using NSubstitute;

namespace HWGA.Tests;

public class Car
{
    public string Color { get; set; } = "Black";
    public bool IsDirty { get; set; } = true;
}

public interface IGarage
{
    bool TryRepaint(Car car, ref int cash);
}

public class PayNSpray : IGarage
{
    public bool TryRepaint(Car car, ref int cash)
    {
        if (cash < 100) return false;
        
        cash -= 100;
        car.Color = "Green";
        car.IsDirty = false; // Мастер еще и помыл её бонусом
        return true;
    }
}

public class CJ
{
    public int Cash { get; set; }
    public Car MyRide { get; set; } = new Car();

    public bool VisitGarage(IGarage garage)
    {
        int moneyInHand = Cash;
        if (garage.TryRepaint(MyRide, ref moneyInHand))
        {
            Cash = moneyInHand;
            return true;
        }

        return false;
    }
}

public class UnitTest2
{
    [Fact]
    public async Task Mission_Accomplished_PayNSpray_Works_Correctly()
    {
        // Arrange
        var cj = new CJ { Cash = 500 };
        var mockGarage = Substitute.For<IGarage>();
        
        mockGarage
            .TryRepaint(cj.MyRide, ref Arg.Any<int>())
            .Returns(x => {
                var car = (Car)x[0];
                x[1] = 400;
                car.Color = "Green";
                car.IsDirty = false;
                return true;
            });

        // Act
        cj.VisitGarage(mockGarage);

        // Assert
        mockGarage.Received(1).TryRepaint(cj.MyRide, ref Arg.Any<int>());
        Assert.Equal(400, cj.Cash);
        Assert.Equal("Green", cj.MyRide.Color);  // Цвет банды
        Assert.False(cj.MyRide.IsDirty);         // ТАЧКА ЧИСТАЯ!
    }

    [Fact]
    public void CJ_Should_Keep_Every_Cent_When_Garage_Refuses_Service()
    {
        var badGarage = Substitute.For<IGarage>();
        var cj = new CJ { Cash = 1000 };
        var initialColor = cj.MyRide.Color;

        badGarage
            .TryRepaint(Arg.Any<Car>(), ref Arg.Any<int>())
            .Returns(false);

        // Act (Попытка заехать в сомнительный сервис)
        cj.VisitGarage(badGarage);

        // Assert (Проверка: Си-Джей всё ещё при деньгах и тачка не перекрашена)
        // Проверяем, что Си-Джей честно пытался договориться
        badGarage.Received(1).TryRepaint(cj.MyRide, ref Arg.Any<int>());
        Assert.Equal(1000, cj.Cash);               // Кэш не тронут
        Assert.Equal(initialColor, cj.MyRide.Color); // Цвет старый
    }

    [Theory]
    [InlineData(50, false, 50, "Black", true)]   // Нищеброд: денег мало, ничего не меняется
    [InlineData(100, true, 0, "Green", false)]   // Тютелька в тютельку: заплатил всё, тачка блестит
    [InlineData(1000, true, 900, "Green", false)] // Мажор: покрасился и остался при деньгах
    public void PayNSpray_Service_Outcome_Depends_On_Cash(
        int initialCash, 
        bool expectedSuccess, 
        int expectedCash, 
        string expectedColor, 
        bool expectedDirty)
    {
        // Arrange
        var mockGarage = Substitute.For<IGarage>();
        var cj = new CJ { Cash = initialCash };
        cj.MyRide.Color = "Black";
        cj.MyRide.IsDirty = true;

        // Настраиваем Mock: логика зависит от входного кэша
        mockGarage
            .TryRepaint(cj.MyRide, ref Arg.Any<int>())
            .Returns(x => {
                int cashInHand = (int)x[1];
                if (cashInHand >= 100)
                {
                    var car = (Car)x[0];
                    car.Color = "Green";
                    car.IsDirty = false;
                    x[1] = cashInHand - 100; // Списываем сотку
                    return true;
                }
                return false;
            });

        // Act
        var actualSuccess = cj.VisitGarage(mockGarage);

        // Assert
        Assert.Equal(expectedSuccess, actualSuccess);
        Assert.Equal(expectedCash, cj.Cash);
        Assert.Equal(expectedColor, cj.MyRide.Color);
        Assert.Equal(expectedDirty, cj.MyRide.IsDirty);
    }
}
