Zawiera implementacje interfejsów z Application — dostęp do bazy danych, plików, API zewnętrznych, itp.

Przykładowe elementy:

Persistence – EF Core DbContext, migracje, konfiguracje encji

Services – np. EmailService, JwtTokenService

Identity – obsługa użytkowników, ról, autoryzacji

DependencyInjection.cs – rejestracja serwisów w kontenerze