function letrasFaltando(texto) {
	texto = texto.toLowerCase();
	var falta = [];

	var alfabeto = ['a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'];

  for(let letra of alfabeto) {
    if(texto.indexOf(letra) === -1) {
      falta.push(letra);
    }
  }

	return falta;
}
