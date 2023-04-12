var titulo = document.querySelector('h1');
var campoHtml = document.querySelectorAll('td');

var Jogo = {
  iniciar() {
    campos = [
      ['', '', ''],
      ['', '', ''],
      ['', '', '']
    ];
    jogadorAtual = 'X';
    rodada = 0;
    jogoContinua = true;
    titulo.innerHTML = `Jogador Atual: ${jogadorAtual}`;
  },

  reiniciar() {
    for(i=0; i<campoHtml.length; i++) {
      campoHtml[i].innerHTML = '';
    }
    this.iniciar();
  },

  proximoJogador() {
    rodada++;
    jogoContinua = this.jogo();
    if (jogoContinua) {
      jogadorAtual = (
        jogadorAtual === 'X' ? 'O' : 'X'
      );
      titulo.innerHTML = `Jogador Atual: ${jogadorAtual}`;
    } else {
      titulo.innerHTML = (
        rodada === 9 ? "Empate" : `Vencedor: ${jogadorAtual}`
      );
    }
  },

  marcar() {
    for(i=0; i<campoHtml.length; i++) {
      campoHtml[i].addEventListener("dblclick", (event) => {
        if (jogoContinua && event.target.innerHTML === '') {
          event.target.innerHTML = jogadorAtual;
          id = event.target.id;
          campos[id[0]][id[1]] = jogadorAtual;
          this.proximoJogador();
        };
      });
    };
  },

  jogo() {
    // Verificar número de rodadas
    if (rodada === 9) {
      return false;  // Jogo finalizou
    }

    // Verificar pontuação horizontal
    for(var i=0; i<3; i++) {
      var pontuacaoX = 0;
      var pontuacaoO = 0;

      for(var j=0; j<3; j++) {
        if(campos[i][j] === 'X' || campos[i][j] === 'O') {
          if (campos[i][j] === 'X')
            pontuacaoX++;
          else
            pontuacaoO++;
        };
      };
      if (pontuacaoX === 3 || pontuacaoO === 3) {
        return false;  // Jogo finalizou
      };
    };

    // Verificar pontuação vertical
    for(var i=0; i<3; i++) {
      var pontuacaoX = 0;
      var pontuacaoO = 0;

      for(var j=0; j<3; j++) {
        if(campos[j][i] === 'X' || campos[j][i] === 'O') {
          if (campos[j][i] === 'X')
            pontuacaoX++;
          else
            pontuacaoO++;
        };
      };
      if (pontuacaoX === 3 || pontuacaoO === 3) { 
        return false;  // Jogo finalizou 
      };
    };

    // Verificar pontuação diagonal principal
    for(
      var i=0, pontuacaoX = 0, pontuacaoO = 0;
      i<3;
      i++
    ) {
      if(campos[i][i] === 'X' || campos[i][i] === 'O') {
        if (campos[i][i] === 'X')
          pontuacaoX++;
        else
          pontuacaoO++;
      };
      if (pontuacaoX === 3 || pontuacaoO === 3) {
        return false;  // Jogo finalizou 
      };
    };

    // Verificar pontuação diagonal secundária
    for(
      var i=0, j=2, pontuacaoX = 0, pontuacaoO = 0;
      i<3;
      i++, j--
    ) {
      if(campos[i][j] === 'X' || campos[i][j] === 'O') {
        if (campos[i][j] === 'X')
          pontuacaoX++;
        else
          pontuacaoO++;
      };
      if (pontuacaoX === 3 || pontuacaoO === 3) {
        return false;  // Jogo finalizou 
      };
    }

    // Jogo continua
    return true;
  }
};

Jogo.iniciar();
