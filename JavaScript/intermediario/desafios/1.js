function Rect(b, h) {
  this.b = b;
  this.h = h;

  this.area = function() {
    return this.b * this.h;
  }
}

function Square(l) {
  this.b = l;
  this.h = l;
}

Square.prototype = new Rect();
