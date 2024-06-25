mergeInto(LibraryManager.library, {
  /*MessageToWeb: function (message, param) {
    window["GetMessageFromUnity"](UTF8ToString(message), UTF8ToString(param));
  },*/

  CopyToClipboard: function (text) {
    navigator.clipboard.writeText(UTF8ToString(text)).then(function() {
      console.log('Text copied to clipboard successfully!');
    }).catch(function(err) {
      console.error('Could not copy text: ', err);
	  console.log(text);
    });
  }
});