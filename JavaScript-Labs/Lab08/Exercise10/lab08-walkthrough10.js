function Order(product, price, quantity) {
  this.product = product;
  this.price = price;
  this.quantity = quantity;

  this.total = function () {
    return this.price * this.quantity;
  };

  this.output = function () {
    // Dữ liệu của bài là hằng số mẫu, không nhận HTML từ người dùng.
    document.write("<article class='lab08-tile'><dl class='lab08-values'>");
    document.write("<dt>Product</dt><dd>" + this.product + "</dd>");
    document.write("<dt>Price</dt><dd>" + this.price + "</dd>");
    document.write("<dt>Quantity</dt><dd>" + this.quantity + "</dd>");
    document.write("<dt>Total</dt><dd>" + this.total() + "</dd>");
    document.write("</dl></article>");
  };
}

const example1 = new Order("Self Portrait in a Straw Hat", 15, 2);
const example2 = new Order("Untitled #23", 10, 4);
example1.output();
example2.output();
