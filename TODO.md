# HWGA
This is an example of TODO.md

View the raw content of this file to understand the format.

### In Progress
- [ ] Статический анализ кода. Добавьте в GitHub Actions:  
  - [ ] SonarCloud — проверка качества кода, поиск багов и уязвимостей;
  - [ ] CodeQL — поиск уязвимостей безопасности.
  - [ ] docs: Бейджики в ридми со статик анализом

```YAML
name: Static Analysis
on:
  push:
    branches: [ main ]
  pull_request:
    types: [opened, synchronize, reopened]
jobs:
  sonarcloud:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
        with:
          fetch-depth: 0  # Важно для SonarCloud

      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '10.0.x'

      - name: Install SonarCloud scanner
        run: dotnet tool install --global dotnet-sonarscanner

      - name: Build and analyze
        env:
          GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
          SONAR_TOKEN: ${{ secrets.SONAR_TOKEN }}
        run: |
          dotnet-sonarscanner begin /k:"ТВОЙ_КЛЮЧ_ПРОЕКТА" /o:"ТВОЯ_ОРГАНИЗАЦИЯ" /d:sonar.token="${{ secrets.SONAR_TOKEN }}" /d:sonar.host.url="https://sonarcloud.io"
          dotnet build src/HWGA.slnx
          dotnet-sonarscanner end /d:sonar.token="${{ secrets.SONAR_TOKEN }}"

```

### Done ✓
- [x] Work on Github ISSUE #1  
- [x] docs: Таблица прогресса README
- [x] ci: Build project on push/PR
- [x] Покрытие тестами.
  - [x] Настройте генерацию отчёта о покрытии и публикуйте его в артефактах Actions.
  - [x] docs: Бейджики в ридми c покрытием тестами