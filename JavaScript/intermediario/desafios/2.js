class Rect {
  constructor(b, h) {
    this.b = b;
    this.h = h;
  }
  area() {
    return this.b * this.h;
  }
}

class Square extends Rect {
  constructor(l) {
    super(l, l);
  }
}
