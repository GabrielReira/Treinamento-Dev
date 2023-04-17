var myObj = {
  _x: 0,
  get x() {
    return this._x;
  },
  set x(value) {
    if (value % 2 !== 0) {
      throw new Error();
    }
    this._x = value;
  }
}
