const CURRENCY_FORMATTER = new Intl.NumberFormat("en-US", {
  style: 'currency',
  currency: 'USD',
  currencyDisplay: 'narrowSymbol'
})

export function formatCurrency(number) {
  return CURRENCY_FORMATTER.format(number);
}