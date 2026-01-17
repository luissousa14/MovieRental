namespace MovieRental.PaymentProviders
{
    public static class PaymentProviderFactory
    {
        public static IPaymentProvider GetProvider(string paymentMethod)
        {
            switch (paymentMethod)
            {
                case "MbWayProvider":
                    return new MbWayProvider();
                case "PayPalProvider":
                    return new PayPalProvider();
                default:
                    throw new NotSupportedException($"Método de pagamento '{paymentMethod}' não suportado");
            }
        }
    }

}
