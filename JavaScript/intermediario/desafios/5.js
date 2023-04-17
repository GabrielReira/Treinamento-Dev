function sum(n1) {
  return function(n2) {
    return n1 + n2;
  }
}

sum(2)(5);
