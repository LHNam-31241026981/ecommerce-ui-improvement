const isCanadian = true;

function calculateTax(amount, tax) {
  return amount * tax();
}

function calculateTotal(price, quantity) {
  const amount = price * quantity;
  return amount + calculateTax(amount, function () {
    if (isCanadian) {
      return 0.05;
    }
    return 0;
  });
}
