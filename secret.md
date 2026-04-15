# Configuration : dotnet user-secrets

- nettoyage d'un ou plusieurs secrets existants : 
```bash
dotnet user-secrets clear

dotnet user-secrets remove "ConnectionStrings:DefaultDb"
```


- Affichage des secrets existants : 
```bash
dotnet user-secrets list
```

- Création d'un nouveau secrets : 
```bash
dotnet user-secrets set "ConnectionStrings:DefaultDb" "Server=localhost;Password=monMdp"
```

