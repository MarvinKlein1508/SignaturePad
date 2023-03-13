import Sigpad from "./sigpad.min.js"

export function setup(id, reference, options, image) {
    var identifier = "signature-" + id;
    var element = document.getElementById(identifier);

    element.addEventListener("sigpad.finish", function (e) {
        reference.invokeMethodAsync('SignatureDataChangedAsync');
    });

    var sigpad = Sigpad.getOrCreateInstance(element, JSON.parse(options));
    sigpad.setImage(image);
}

export function destroy(id) {
    var identifier = "signature-" + id;
    var element = document.getElementById(identifier);

    var sigpad = Sigpad.getInstance(element);
    if (sigpad != null) {
        sigpad.destroy();
    }
}

export function update(id, options) {
    var identifier = "signature-" + id;
    var element = document.getElementById(identifier);

    var sigpad = Sigpad.getOrCreateInstance(element, JSON.parse(options));
    sigpad._config = sigpad._getConfig(JSON.parse(options));
    sigpad._applyOptions();
}

export function updateImage(id, image) {
    var identifier = "signature-" + id;
    var element = document.getElementById(identifier);

    var sigpad = Sigpad.getInstance(element);
    if (sigpad != null) {
        sigpad.setImage(image);
    }
}
export function clear(id) {
    var identifier = "signature-" + id;
    var element = document.getElementById(identifier);

    var sigpad = Sigpad.getInstance(element);
    if (sigpad != null) {
        sigpad.clear();
    }
}

export function getBase64(id) {
    var identifier = "signature-" + id;
    var element = document.getElementById(identifier);

    var sigpad = Sigpad.getInstance(element);

    var base64 = sigpad.getImage();


    var binary_string = window.btoa(base64)
    var len = binary_string.length;
    var bytes = new Uint8Array(len);
    for (var i = 0; i < len; i++) {
        bytes[i] = binary_string.charCodeAt(i);
    }
    return bytes.buffer;

}