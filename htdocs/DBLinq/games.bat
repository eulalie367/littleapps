cd C:\Program Files\Microsoft SDKs\Windows\v6.0A\bin\

sqlmetal /conn:"Integrated Security=true;Initial Catalog=Games;Server=DELLLAPTOP" /namespace:Gamespot.DB.Games /code:C:/DBLinq/Games.cs /language:csharp /pluralize /views /functions /sprocs /context:dcGames 

