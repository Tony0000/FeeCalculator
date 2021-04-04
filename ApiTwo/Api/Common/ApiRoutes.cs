namespace Api.Common
{
    public static class ApiRoutes
    {
        public static class FeeCalculator
        {
            private const string BaseUrl = "";
            public const string CalcFee = "/calculajuros";
            public const string ShowMeTheCode = "/showmethecode";
        }

        public static class ApiOne
        {
            public const string TaxaJuros = "/taxajuros";
        }
    }

    public static class StaticUrls
    {
        public const string GithubRepository = "https://github.com/Tony0000/FeeCalculator";
    }
}