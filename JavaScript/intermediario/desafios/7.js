function handleSelectedFile() {
  var file = document.querySelector('#myFile').files[0];
  var reader = new FileReader();

  reader.onload = function() {
    var content = reader.result;
    document.querySelector('#fileContent').value = content;
  }
  reader.readAsText(file);
}

function saveFile() {
  var element = document.createElement('a');
  var content = document.querySelector('#fileContent').value;

  element.setAttribute('href', 'data:plain/text;charset=utf-8,' + encodeURIComponent(content));
  element.setAttribute('download', 'myNewFile.txt');
  element.click();
}
