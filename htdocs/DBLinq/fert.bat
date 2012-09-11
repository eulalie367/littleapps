cd C:\Program Files\Microsoft SDKs\Windows\v6.0A\bin\

sqlmetal /conn:"Integrated Security=true;Initial Catalog=Fertilizer;Server=DELLLAPTOP" /namespace:Calculators.Fertilizer /code:C:/DBLinq/Fertilizer.cs /language:csharp /pluralize /views /functions /sprocs /context:dcFertilizer 

