var Tabela = {
  campos: [
    ['X', ' ', ' '],
    [' ', ' ', ' '],
    [' ', ' ', ' ']
  ],
  posicao: [0,0],

  // Direção: vertical(0) || horizontal (1)
  // Sentido: cima/esquerda (0) || baixo/direita (1)
  atualizaX(direcao, sentido) {  
    this.campos[this.posicao[0]][this.posicao[1]] = ' ';  // Remover o X
    // Atualizar posição do X
    if(sentido === 0) {
      this.posicao[direcao]--;
    } else {
      this.posicao[direcao]++;
    }
    this.campos[this.posicao[0]][this.posicao[1]] = 'X';  // Adicionar o X
    this.print();
  },

  up() {
    if(this.posicao[0] > 0) {
      this.atualizaX(0, 0);
    }
  },
  down() {
    if(this.posicao[0] < this.campos.length-1) {
      this.atualizaX(0, 1);
    }
  },
  left() {
    if(this.posicao[1] > 0) {
      this.atualizaX(1, 0);
    }
  },
  right() {
    if(this.posicao[1] < this.campos[this.posicao[0]].length-1) {
      this.atualizaX(1, 1);
    }
  },

  print() {
    for(var i=0; i<this.campos.length; i++) {
      var linha = '';
      for(var j=0; j<this.campos.length; j++){
        linha += `| ${this.campos[i][j]} |`
      }
      console.log(linha)
    }
  }
}


// SOLUÇÃO DO PROFESSOR
var Table = {
	field: [
		['','',''],
		['','',''],
		['','','']
	],
	position: [0,0],
	up(){
		if(this.position[0] > 0){
			this.position[0]--;
		}
		this.print();
	},
	down(){
		if(this.position[0] < this.field.length-1){
			this.position[0]++;
		}
		this.print();
	},
	left(){
		if(this.position[1] > 0){
			this.position[1]--;
		}
		this.print();
	},
	right(){
		if(this.position[1] < this.field[this.position[0]].length-1){
			this.position[1]++;
		}
		this.print();
	},
	print(){
		var lineStr = '';
		for(var i = 0; i < this.field.length; i++){
			var line = this.field[i];
			for(var j = 0; j < line.length; j++){
				if(this.position[0] === i && this.position[1] === j){
					lineStr += '| X |';
				}else{
					lineStr += '|   |';
				}
			}
			lineStr += '\n';
		}
		console.log(lineStr);
	}
}
