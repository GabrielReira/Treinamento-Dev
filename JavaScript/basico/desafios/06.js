var Elevador = {
  andarAtual: 0,
  andarMinimo: -2,
  andarMaximo: 7,

  up() {
    if(this.andarAtual < this.andarMaximo) {
      this.andarAtual++;
    }
    this.print();
  },
  down() {
    if(this.andarAtual > this.andarMinimo) {
      this.andarAtual--;
    }
    this.print();
  },

  print() {
    console.log(`Andar: ${this.andarAtual}`)
  }
}
