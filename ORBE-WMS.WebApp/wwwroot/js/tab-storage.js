// Tab persistence helpers for sessionStorage
window.tabStorage = {
    save: function (tabsJson) {
        sessionStorage.setItem('orbe_tabs', tabsJson);
    },
    load: function () {
        return sessionStorage.getItem('orbe_tabs') || '[]';
    }
};
