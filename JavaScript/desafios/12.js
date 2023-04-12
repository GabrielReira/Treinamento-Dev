var lista = [1, 2, 3, 'Sete'];
var elementos = document.querySelector('ul');

function gerarLista() {
  var html = '';
  for(let item of lista) {
    html += `<li>Item: ${item}</li>`;
  }
  elementos.innerHTML = html;
}
