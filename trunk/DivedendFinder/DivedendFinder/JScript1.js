GoogleColumn = function()
{
    this.display_name;
    this.value;
    this.field;
    this.sort_order
}
GoogleCompany = function()
{
    this.title;
    this.id;
    this.is_active;
    this.ticker;
    this.exchange;
    this.is_supported_exchange;
    this.columns = [GoogleColumn];
}
GoogleReturn = function ()
{
    this.Start;
    this.num_company_results;
    this.num_mf_results;
    this.num_all_results;
    this.original_query;
    this.query_for_display;
    this.results_type;
    this.results_display_type;
    this.searchresults = [GoogleCompany];
}
var g = new GoogleReturn();
g = 
{
"start" : "20",
"num_company_results" : "21",
"num_mf_results" : "0",
"num_all_results" : "",
"original_query" : "((exchange:NYSE) OR (exchange:NASDAQ) OR (exchange:AMEX)) [(MarketCap \x3E 3810000 | MarketCap = 3810000) \x26 (MarketCap \x3C 9720000 | MarketCap = 9720000) \x26 (PE \x3E 0.04 | PE = 0.04) \x26 (PE \x3C 8263 | PE = 8263) \x26 (DividendYield \x3E 0 | DividendYield = 0) \x26 (DividendYield \x3C 486 | DividendYield = 486) \x26 (Price52WeekPercChange \x3E -99.83 | Price52WeekPercChange = -99.83) \x26 (Price52WeekPercChange \x3C 5075 | Price52WeekPercChange = 5075)]",
"query_for_display" : "((exchange:NYSE) OR (exchange:NASDAQ) OR (exchange:AMEX)) [(MarketCap &gt; 3810000 | MarketCap = 3810000) &amp; (MarketCap &lt; 9720000 | MarketCap = 9720000) &amp; (PE &gt; 0.04 | PE = 0.04) &amp; (PE &lt; 8263 | PE = 8263) &amp; (DividendYield &gt; 0 | DividendYield = 0) &amp; (DividendYield &lt; 486 | DividendYield = 486) &amp; (Price52WeekPercChange &gt; -99.83 | Price52WeekPercChange = -99.83) &amp; (Price52WeekPercChange &lt; 5075 | Price52WeekPercChange = 5075)]",
"results_type" : "COMPANY",
"results_display_type" : "TABLE",
"searchresults" :
[
{
"title" : "WSI Industries, Inc.",
"id" : "611424",
"is_active" : "",
"ticker" : "WSCI",
"exchange" : "NASDAQ",
"is_supported_exchange" : "",
"columns" : [
{
"display_name": "",
"value" : "7.20M",
"field" : "MarketCap",
"sort_order" : ""
}
, {
"display_name": "",
"value" : "15.93",
"field" : "PE",
"sort_order" : ""
}
, {
"display_name": "",
"value" : "5.77",
"field" : "DividendYield",
"sort_order" : ""
}
, {
"display_name": "",
"value" : "-79.40",
"field" : "Price52WeekPercChange",
"sort_order" : ""
}
]
}
]
,
"mf_searchresults" :
[
]
};
asdf
