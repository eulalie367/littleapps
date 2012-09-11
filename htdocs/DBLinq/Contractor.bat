cd C:\Program Files\Microsoft SDKs\Windows\v6.0A\bin\

sqlmetal /conn:"Integrated Security=true;Initial Catalog=Contractor;Server=DELLLAPTOP" /namespace:Contractor /code:C:/DBLinq/Contractor.cs /language:csharp /pluralize /views /functions /sprocs /context:dcContractor 

