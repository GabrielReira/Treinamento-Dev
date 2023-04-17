var myWindow = null;
var urlElement = document.querySelector('#myUrl');

function go() {
  var url = 'http://' + urlElement.value;
  if(myWindow && !myWindow.closed) {
    myWindow.location.href = url;
  } else {
    myWindow = window.open(url, 'New Window', 'width=500, height = 300');
  }
}
