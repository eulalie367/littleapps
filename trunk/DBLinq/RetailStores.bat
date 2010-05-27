cd C:\Program Files\Microsoft SDKs\Windows\v6.0A\bin\

sqlmetal /conn:"Integrated Security=true;Initial Catalog=RetailStores;Server=DELLLAPTOP" /namespace:RetailStores /code:C:/DBLinq/RetailStores.cs /language:csharp /views /functions /sprocs /context:dcRetailStores 

pause
