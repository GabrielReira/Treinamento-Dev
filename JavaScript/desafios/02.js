switch (letra) {
  case 'a': 
    vogal = true;
    break;
  case 'e': 
    vogal = true;
    break;
  case 'i': 
    vogal = true;
    break;
  case 'o': 
    vogal = true;
    break;
  case 'u': 
    vogal = true;
    break;
  default:
    vogal = false;
}

if (vogal) {
  console.log('É vogal.')
} else {
  console.log('É consoante.')
}
