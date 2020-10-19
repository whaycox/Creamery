window.parmesan = {

    generateRandom: function (byteLength) {
        var byteString = '';
        var bytes = new Uint8Array(byteLength);
        window.crypto.getRandomValues(bytes);
        for (var i = 0; i < byteLength; i++) {
            byteString += String.fromCharCode(bytes[i]);
        }
        return btoa(byteString);
    },

    storeLoginRequest: function (verifier, state) {
        var path = "; path=/loginRedirect";
        var verifierCookie = "parmesan.loginVerifier=" + verifier + path;
        var stateCookie = "parmesan.loginState=" + state + path;

        document.cookie = verifierCookie;
        document.cookie = stateCookie;
    },

    consumeLoginRequest: function (cookieName) {
        var path = "; path=/loginRedirect";
        var cookies = document.cookie.split(";");
        for (var i = 0; i < cookies.length; i++) {
            var pair = cookies[i].split("=");
            if (pair[0].trim() === cookieName) {
                document.cookie = cookieName + "=" + path + "; max-age=0";
                return pair[1];
            }
        }
    }

}