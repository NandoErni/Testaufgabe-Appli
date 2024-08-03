# Testaufgabe-Appli

## .Net Application
Die Applikation läuft auf Port 7254 (https) oder 5198 (http)
Dokumentation wurde mittels Swagger umgesetzt: [URL]/swagger (https://localhost:7254)
Bei Swagger wurde aber kaum Zeit investiert, da es nur mittel zum Zweck ist. (--> Enums und so sind nicht schön dargestellt)

Als db kommt SQLite zum Einsatz. Anbindung läuft über das EF Core (Code first)

### Projekt struktur
- ASP.NET App ist im Projekt "testaufgabe"
  - DB Create Scripts sind im Ordner "Migration"
- Alle Unit-Tests sind im Projekt "Tests"

### Techstack
- .Net 8
- EF Core 8
- SQLite


## Testing
### Techstack
- xunit
- Moq
- EF Core InMemory database