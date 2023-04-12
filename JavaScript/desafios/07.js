function mensagem() {
  var num = Math.trunc((Math.random() * 3) + 1);

  switch (num) {
    case 1: console.log('Como vai você?'); break;
    case 2: console.log('How are you?'); break;
    case 3: console.log('Wie geht es ihnen?'); break;
  }
}