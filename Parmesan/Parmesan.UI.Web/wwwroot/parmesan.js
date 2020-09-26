window.redirectTo = (destinationUrl) => window.location = destinationUrl;

window.generateRandom = (byteLength) => {
    var byteString = '';
    var bytes = new Uint8Array(byteLength);
    window.crypto.getRandomValues(bytes);
    for (var i = 0; i < byteLength; i++) {
        byteString += String.fromCharCode(bytes[i]);
    }
    return btoa(byteString);
}