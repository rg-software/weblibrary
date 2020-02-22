chrome.browserAction.onClicked.addListener(buttonClicked);

function buttonClicked(tab) {
  chrome.tabs.query({'active': true, 'lastFocusedWindow': true}, function (tabs) {
    var cur_url = tabs[0].url;

    chrome.runtime.sendMessage('bifmfjgpgndemajpeeoiopbeilbaifdo', {
      app: {
        args: '[HREF]',
        path: 'WebLibraryDownloader'
      },
      tab: {
        url: cur_url
      }
    });
  });

    // The user clicked the button!
  // 'tab' is an object with information about the current open tab
}