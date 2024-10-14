(function () {
    "use strict";

    function injectProxy(
        enableStyleSheetsProxy,
        enableCustomElementRegistryProxy
    ) {
        document.dispatchEvent(
            new CustomEvent("__darkreader__inlineScriptsAllowed")
        );
        const addRuleDescriptor = Object.getOwnPropertyDescriptor(
            CSSStyleSheet.prototype,
            "addRule"
        );
        const insertRuleDescriptor = Object.getOwnPropertyDescriptor(
            CSSStyleSheet.prototype,
            "insertRule"
        );
        const deleteRuleDescriptor = Object.getOwnPropertyDescriptor(
            CSSStyleSheet.prototype,
            "deleteRule"
        );
        const removeRuleDescriptor = Object.getOwnPropertyDescriptor(
            CSSStyleSheet.prototype,
            "removeRule"
        );
        const replaceDescriptor = Object.getOwnPropertyDescriptor(
            CSSStyleSheet.prototype,
            "replace"
        );
        const replaceSyncDescriptor = Object.getOwnPropertyDescriptor(
            CSSStyleSheet.prototype,
            "replaceSync"
        );
        const documentStyleSheetsDescriptor = enableStyleSheetsProxy
            ? Object.getOwnPropertyDescriptor(Document.prototype, "styleSheets")
            : null;
        const customElementRegistryDefineDescriptor =
            enableCustomElementRegistryProxy
                ? Object.getOwnPropertyDescriptor(
                      CustomElementRegistry.prototype,
                      "define"
                  )
                : null;
        const shouldWrapHTMLElement = [
            "baidu.com",
            "baike.baidu.com",
            "ditu.baidu.com",
            "map.baidu.com",
            "maps.baidu.com",
            "haokan.baidu.com",
            "pan.baidu.com",
            "passport.baidu.com",
            "tieba.baidu.com",
            "www.baidu.com"
        ].includes(location.hostname);
        const getElementsByTagNameDescriptor = shouldWrapHTMLElement
            ? Object.getOwnPropertyDescriptor(
                  Element.prototype,
                  "getElementsByTagName"
              )
            : null;
        const shouldProxyChildNodes = location.hostname === "www.vy.no";
        const childNodesDescriptor = shouldProxyChildNodes
            ? Object.getOwnPropertyDescriptor(Node.prototype, "childNodes")
            : null;
        const cleaners = [];
        const cleanUp = () => {
            Object.defineProperty(
                CSSStyleSheet.prototype,
                "addRule",
                addRuleDescriptor
            );
            Object.defineProperty(
                CSSStyleSheet.prototype,
                "insertRule",
                insertRuleDescriptor
            );
            Object.defineProperty(
                CSSStyleSheet.prototype,
                "deleteRule",
                deleteRuleDescriptor
            );
            Object.defineProperty(
                CSSStyleSheet.prototype,
                "removeRule",
                removeRuleDescriptor
            );
            Object.defineProperty(
                CSSStyleSheet.prototype,
                "replace",
                replaceDescriptor
            );
            Object.defineProperty(
                CSSStyleSheet.prototype,
                "replaceSync",
                replaceSyncDescriptor
            );
            document.removeEventListener("__darkreader__cleanUp", cleanUp);
            document.removeEventListener(
                "__darkreader__addUndefinedResolver",
                addUndefinedResolver
            );
            document.removeEventListener(
                "__darkreader__blobURLCheckRequest",
                checkBlobURLSupport
            );
            if (enableStyleSheetsProxy) {
                Object.defineProperty(
                    Document.prototype,
                    "styleSheets",
                    documentStyleSheetsDescriptor
                );
            }
            if (enableCustomElementRegistryProxy) {
                Object.defineProperty(
                    CustomElementRegistry.prototype,
                    "define",
                    customElementRegistryDefineDescriptor
                );
            }
            if (shouldWrapHTMLElement) {
                Object.defineProperty(
                    Element.prototype,
                    "getElementsByTagName",
                    getElementsByTagNameDescriptor
                );
            }
            if (shouldProxyChildNodes) {
                Object.defineProperty(
                    Node.prototype,
                    "childNodes",
                    childNodesDescriptor
                );
            }
            cleaners.forEach((clean) => clean());
            cleaners.splice(0);
        };
        const addUndefinedResolverInner = (tag) => {
            customElements.whenDefined(tag).then(() => {
                document.dispatchEvent(
                    new CustomEvent("__darkreader__isDefined", {detail: {tag}})
                );
            });
        };
        const addUndefinedResolver = (e) =>
            addUndefinedResolverInner(e.detail.tag);
        document.addEventListener("__darkreader__cleanUp", cleanUp, {
            passive: true
        });
        document.addEventListener(
            "__darkreader__addUndefinedResolver",
            addUndefinedResolver,
            {passive: true}
        );
        document.addEventListener(
            "__darkreader__blobURLCheckRequest",
            checkBlobURLSupport,
            {once: true}
        );
        const updateSheetEvent = new Event("__darkreader__updateSheet");
        function proxyAddRule(selector, style, index) {
            addRuleDescriptor.value.call(this, selector, style, index);
            if (
                this.ownerNode &&
                !(
                    this.ownerNode.classList &&
                    this.ownerNode.classList.contains("darkreader")
                )
            ) {
                this.ownerNode.dispatchEvent(updateSheetEvent);
            }
            return -1;
        }
        function proxyInsertRule(rule, index) {
            const returnValue = insertRuleDescriptor.value.call(
                this,
                rule,
                index
            );
            if (
                this.ownerNode &&
                !(
                    this.ownerNode.classList &&
                    this.ownerNode.classList.contains("darkreader")
                )
            ) {
                this.ownerNode.dispatchEvent(updateSheetEvent);
            }
            return returnValue;
        }
        function proxyDeleteRule(index) {
            deleteRuleDescriptor.value.call(this, index);
            if (
                this.ownerNode &&
                !(
                    this.ownerNode.classList &&
                    this.ownerNode.classList.contains("darkreader")
                )
            ) {
                this.ownerNode.dispatchEvent(updateSheetEvent);
            }
        }
        function proxyRemoveRule(index) {
            removeRuleDescriptor.value.call(this, index);
            if (
                this.ownerNode &&
                !(
                    this.ownerNode.classList &&
                    this.ownerNode.classList.contains("darkreader")
                )
            ) {
                this.ownerNode.dispatchEvent(updateSheetEvent);
            }
        }
        function proxyReplace(cssText) {
            const returnValue = replaceDescriptor.value.call(this, cssText);
            if (
                this.ownerNode &&
                !(
                    this.ownerNode.classList &&
                    this.ownerNode.classList.contains("darkreader")
                ) &&
                returnValue &&
                returnValue instanceof Promise
            ) {
                returnValue.then(() =>
                    this.ownerNode.dispatchEvent(updateSheetEvent)
                );
            }
            return returnValue;
        }
        function proxyReplaceSync(cssText) {
            replaceSyncDescriptor.value.call(this, cssText);
            if (
                this.ownerNode &&
                !(
                    this.ownerNode.classList &&
                    this.ownerNode.classList.contains("darkreader")
                )
            ) {
                this.ownerNode.dispatchEvent(updateSheetEvent);
            }
        }
        function proxyDocumentStyleSheets() {
            const getCurrentValue = () => {
                const docSheets = documentStyleSheetsDescriptor.get.call(this);
                const filteredSheets = [...docSheets].filter(
                    (styleSheet) =>
                        styleSheet.ownerNode &&
                        !(
                            styleSheet.ownerNode.classList &&
                            styleSheet.ownerNode.classList.contains(
                                "darkreader"
                            )
                        )
                );
                filteredSheets.item = (item) => filteredSheets[item];
                return Object.setPrototypeOf(
                    filteredSheets,
                    StyleSheetList.prototype
                );
            };
            let elements = getCurrentValue();
            const styleSheetListBehavior = {
                get: function (_, property) {
                    return getCurrentValue()[property];
                }
            };
            elements = new Proxy(elements, styleSheetListBehavior);
            return elements;
        }
        function proxyCustomElementRegistryDefine(name, constructor, options) {
            addUndefinedResolverInner(name);
            customElementRegistryDefineDescriptor.value.call(
                this,
                name,
                constructor,
                options
            );
        }
        function proxyGetElementsByTagName(tagName) {
            if (tagName !== "style") {
                return getElementsByTagNameDescriptor.value.call(this, tagName);
            }
            const getCurrentElementValue = () => {
                const elements = getElementsByTagNameDescriptor.value.call(
                    this,
                    tagName
                );
                return Object.setPrototypeOf(
                    [...elements].filter(
                        (element) =>
                            element &&
                            !(
                                element.classList &&
                                element.classList.contains("darkreader")
                            )
                    ),
                    NodeList.prototype
                );
            };
            let elements = getCurrentElementValue();
            const nodeListBehavior = {
                get: function (_, property) {
                    return getCurrentElementValue()[
                        Number(property) || property
                    ];
                }
            };
            elements = new Proxy(elements, nodeListBehavior);
            return elements;
        }
        function proxyChildNodes() {
            const childNodes = childNodesDescriptor.get.call(this);
            return Object.setPrototypeOf(
                [...childNodes].filter((element) => {
                    return (
                        !element.classList ||
                        !element.classList.contains("darkreader")
                    );
                }),
                NodeList.prototype
            );
        }
        async function checkBlobURLSupport() {
            const svg =
                '<svg xmlns="http://www.w3.org/2000/svg" width="1" height="1"><rect width="1" height="1" fill="transparent"/></svg>';
            const bytes = new Uint8Array(svg.length);
            for (let i = 0; i < svg.length; i++) {
                bytes[i] = svg.charCodeAt(i);
            }
            const blob = new Blob([bytes], {type: "image/svg+xml"});
            const objectURL = URL.createObjectURL(blob);
            let blobURLAllowed;
            try {
                const image = new Image();
                await new Promise((resolve, reject) => {
                    image.onload = () => resolve();
                    image.onerror = () => reject();
                    image.src = objectURL;
                });
                blobURLAllowed = true;
            } catch (err) {
                blobURLAllowed = false;
            }
            document.dispatchEvent(
                new CustomEvent("__darkreader__blobURLCheckResponse", {
                    detail: {blobURLAllowed}
                })
            );
        }
        Object.defineProperty(CSSStyleSheet.prototype, "addRule", {
            ...addRuleDescriptor,
            value: proxyAddRule
        });
        Object.defineProperty(CSSStyleSheet.prototype, "insertRule", {
            ...insertRuleDescriptor,
            value: proxyInsertRule
        });
        Object.defineProperty(CSSStyleSheet.prototype, "deleteRule", {
            ...deleteRuleDescriptor,
            value: proxyDeleteRule
        });
        Object.defineProperty(CSSStyleSheet.prototype, "removeRule", {
            ...removeRuleDescriptor,
            value: proxyRemoveRule
        });
        Object.defineProperty(CSSStyleSheet.prototype, "replace", {
            ...replaceDescriptor,
            value: proxyReplace
        });
        Object.defineProperty(CSSStyleSheet.prototype, "replaceSync", {
            ...replaceSyncDescriptor,
            value: proxyReplaceSync
        });
        if (enableStyleSheetsProxy) {
            Object.defineProperty(Document.prototype, "styleSheets", {
                ...documentStyleSheetsDescriptor,
                get: proxyDocumentStyleSheets
            });
        }
        if (enableCustomElementRegistryProxy) {
            Object.defineProperty(CustomElementRegistry.prototype, "define", {
                ...customElementRegistryDefineDescriptor,
                value: proxyCustomElementRegistryDefine
            });
        }
        if (shouldWrapHTMLElement) {
            Object.defineProperty(Element.prototype, "getElementsByTagName", {
                ...getElementsByTagNameDescriptor,
                value: proxyGetElementsByTagName
            });
        }
        if (shouldProxyChildNodes) {
            Object.defineProperty(Node.prototype, "childNodes", {
                ...childNodesDescriptor,
                get: proxyChildNodes
            });
        }
    }

    var MessageTypeUItoBG;
    (function (MessageTypeUItoBG) {
        MessageTypeUItoBG["GET_DATA"] = "ui-bg-get-data";
        MessageTypeUItoBG["GET_DEVTOOLS_DATA"] = "ui-bg-get-devtools-data";
        MessageTypeUItoBG["SUBSCRIBE_TO_CHANGES"] =
            "ui-bg-subscribe-to-changes";
        MessageTypeUItoBG["UNSUBSCRIBE_FROM_CHANGES"] =
            "ui-bg-unsubscribe-from-changes";
        MessageTypeUItoBG["CHANGE_SETTINGS"] = "ui-bg-change-settings";
        MessageTypeUItoBG["SET_THEME"] = "ui-bg-set-theme";
        MessageTypeUItoBG["TOGGLE_ACTIVE_TAB"] = "ui-bg-toggle-active-tab";
        MessageTypeUItoBG["MARK_NEWS_AS_READ"] = "ui-bg-mark-news-as-read";
        MessageTypeUItoBG["MARK_NEWS_AS_DISPLAYED"] =
            "ui-bg-mark-news-as-displayed";
        MessageTypeUItoBG["LOAD_CONFIG"] = "ui-bg-load-config";
        MessageTypeUItoBG["APPLY_DEV_DYNAMIC_THEME_FIXES"] =
            "ui-bg-apply-dev-dynamic-theme-fixes";
        MessageTypeUItoBG["RESET_DEV_DYNAMIC_THEME_FIXES"] =
            "ui-bg-reset-dev-dynamic-theme-fixes";
        MessageTypeUItoBG["APPLY_DEV_INVERSION_FIXES"] =
            "ui-bg-apply-dev-inversion-fixes";
        MessageTypeUItoBG["RESET_DEV_INVERSION_FIXES"] =
            "ui-bg-reset-dev-inversion-fixes";
        MessageTypeUItoBG["APPLY_DEV_STATIC_THEMES"] =
            "ui-bg-apply-dev-static-themes";
        MessageTypeUItoBG["RESET_DEV_STATIC_THEMES"] =
            "ui-bg-reset-dev-static-themes";
        MessageTypeUItoBG["COLOR_SCHEME_CHANGE"] = "ui-bg-color-scheme-change";
        MessageTypeUItoBG["HIDE_HIGHLIGHTS"] = "ui-bg-hide-highlights";
    })(MessageTypeUItoBG || (MessageTypeUItoBG = {}));
    var MessageTypeBGtoUI;
    (function (MessageTypeBGtoUI) {
        MessageTypeBGtoUI["CHANGES"] = "bg-ui-changes";
    })(MessageTypeBGtoUI || (MessageTypeBGtoUI = {}));
    var DebugMessageTypeBGtoUI;
    (function (DebugMessageTypeBGtoUI) {
        DebugMessageTypeBGtoUI["CSS_UPDATE"] = "debug-bg-ui-css-update";
        DebugMessageTypeBGtoUI["UPDATE"] = "debug-bg-ui-update";
    })(DebugMessageTypeBGtoUI || (DebugMessageTypeBGtoUI = {}));
    var MessageTypeBGtoCS;
    (function (MessageTypeBGtoCS) {
        MessageTypeBGtoCS["ADD_CSS_FILTER"] = "bg-cs-add-css-filter";
        MessageTypeBGtoCS["ADD_DYNAMIC_THEME"] = "bg-cs-add-dynamic-theme";
        MessageTypeBGtoCS["ADD_STATIC_THEME"] = "bg-cs-add-static-theme";
        MessageTypeBGtoCS["ADD_SVG_FILTER"] = "bg-cs-add-svg-filter";
        MessageTypeBGtoCS["CLEAN_UP"] = "bg-cs-clean-up";
        MessageTypeBGtoCS["FETCH_RESPONSE"] = "bg-cs-fetch-response";
        MessageTypeBGtoCS["UNSUPPORTED_SENDER"] = "bg-cs-unsupported-sender";
    })(MessageTypeBGtoCS || (MessageTypeBGtoCS = {}));
    var DebugMessageTypeBGtoCS;
    (function (DebugMessageTypeBGtoCS) {
        DebugMessageTypeBGtoCS["RELOAD"] = "debug-bg-cs-reload";
    })(DebugMessageTypeBGtoCS || (DebugMessageTypeBGtoCS = {}));
    var MessageTypeCStoBG;
    (function (MessageTypeCStoBG) {
        MessageTypeCStoBG["COLOR_SCHEME_CHANGE"] = "cs-bg-color-scheme-change";
        MessageTypeCStoBG["DARK_THEME_DETECTED"] = "cs-bg-dark-theme-detected";
        MessageTypeCStoBG["DARK_THEME_NOT_DETECTED"] =
            "cs-bg-dark-theme-not-detected";
        MessageTypeCStoBG["FETCH"] = "cs-bg-fetch";
        MessageTypeCStoBG["DOCUMENT_CONNECT"] = "cs-bg-document-connect";
        MessageTypeCStoBG["DOCUMENT_FORGET"] = "cs-bg-document-forget";
        MessageTypeCStoBG["DOCUMENT_FREEZE"] = "cs-bg-document-freeze";
        MessageTypeCStoBG["DOCUMENT_RESUME"] = "cs-bg-document-resume";
    })(MessageTypeCStoBG || (MessageTypeCStoBG = {}));
    var DebugMessageTypeCStoBG;
    (function (DebugMessageTypeCStoBG) {
        DebugMessageTypeCStoBG["LOG"] = "debug-cs-bg-log";
    })(DebugMessageTypeCStoBG || (DebugMessageTypeCStoBG = {}));
    var MessageTypeCStoUI;
    (function (MessageTypeCStoUI) {
        MessageTypeCStoUI["EXPORT_CSS_RESPONSE"] = "cs-ui-export-css-response";
    })(MessageTypeCStoUI || (MessageTypeCStoUI = {}));
    var MessageTypeUItoCS;
    (function (MessageTypeUItoCS) {
        MessageTypeUItoCS["EXPORT_CSS"] = "ui-cs-export-css";
    })(MessageTypeUItoCS || (MessageTypeUItoCS = {}));

    function logInfo(...args) {}

    document.currentScript && document.currentScript.remove();
    const key = "darkreaderProxyInjected";
    const EVENT_DONE = "__darkreader__stylesheetProxy__done";
    const EVENT_ARG = "__darkreader__stylesheetProxy__arg";
    const registerdScriptPath = !document.currentScript;
    function injectProxyAndCleanup(args) {
        injectProxy(
            args.enableStyleSheetsProxy,
            args.enableCustomElementRegistryProxy
        );
        doneReceiver();
        document.dispatchEvent(new CustomEvent(EVENT_DONE));
    }
    function regularPath() {
        const argString = document.currentScript.dataset.arg;
        if (argString !== undefined) {
            document.documentElement.dataset[key] = "true";
            const args = JSON.parse(argString);
            injectProxyAndCleanup(args);
        }
    }
    function dataReceiver(e) {
        document.removeEventListener(EVENT_ARG, dataReceiver);
        if (document.documentElement.dataset[key] !== undefined) {
            return;
        }
        document.documentElement.dataset[key] = "true";
        logInfo(
            `MV3 proxy injector: ${
                registerdScriptPath ? "registerd" : "dedicated"
            } path runs injectProxy(${e.detail}).`
        );
        injectProxyAndCleanup(e.detail);
    }
    function doneReceiver() {
        document.removeEventListener(EVENT_ARG, dataReceiver);
        document.removeEventListener(EVENT_DONE, doneReceiver);
    }
    function dedicatedPath() {
        const listenerOptions = {
            passive: true,
            once: true
        };
        document.addEventListener(EVENT_ARG, dataReceiver, listenerOptions);
        document.addEventListener(EVENT_DONE, doneReceiver, listenerOptions);
    }
    function inject() {
        if (document.documentElement.dataset[key] !== undefined) {
            return;
        }
        document.currentScript && regularPath();
        dedicatedPath();
    }
    inject();
})();
