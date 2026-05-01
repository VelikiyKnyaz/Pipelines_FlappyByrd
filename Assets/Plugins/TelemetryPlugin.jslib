mergeInto(LibraryManager.library, {
    InitFirebaseBridge: function() {
        if (!window.__firebaseBridgeInit) {
            window.__firebaseBridgeInit = true;
            window.addEventListener("message", function (event) {
                if (event.data && event.data.type === "firebase-auth") {
                    if (window.parent && window.parent !== window) {
                        window.parent.postMessage({ type: "firebase-auth-ack" }, "*");
                        console.log("Telemetry: Sent auth ack to portal");
                    }
                }
            });
            console.log("Telemetry: Listener registered");
        }
    },

    SendGameStartTelemetry: function() {
        try {
            if (window.parent && window.parent !== window) {
                window.parent.postMessage({ type: "game_start" }, "*");
            }
        } catch (e) {}
    },

    SendGameEndTelemetry: function(score, pipesPassed) {
        try {
            if (window.parent && window.parent !== window) {
                window.parent.postMessage({ type: "game_end", score: score, pipesPassed: pipesPassed }, "*");
            }
        } catch (e) {}
    }
});
