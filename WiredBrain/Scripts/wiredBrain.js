setupConnection = (hubProxy) => {
    hubProxy.client.receiveOrderUpate = (updateObject) => {
        const statusDiv = document.getElementById("status");
        statusDiv.innerHTML = `Order: ${updateObject.OrderId} : ${updateObject.Update}`;
    };

    //hubProxy.client.newOrder = (order) => {
    //    const statusDiv = document.getElementById("status");
    //    statusDiv.innerHTML = `Somebody ordered an ${order.Product}`;
    //};

    //hubProxy.logging = true;

    hubProxy.client.finished = (order) => {
        console.log(`Finished coffee order ${order}`);
    };

};


$.connection.hub.logging = true;
$(() => {
    var hubProxy = $.connection.coffeeHub;
    setupConnection(hubProxy);
    $.connection.hub.logging = true;
    $.connection.hub.start();

    document.getElementById("submit").addEventListener("click",
        e => {
            e.preventDefault();
            var statusDiv = document.getElementById("status");
            statusDiv.innerHTML = "Submitting order...";

            const product = document.getElementById("product").value;
            const size = document.getElementById("size").value;

            fetch("api/coffee",
                {
                    method: "POST",
                    body: JSON.stringify({ product, size }),
                    headers: { 'content-type': 'application/json' }
                })
                .then(res => res.text())
                .then(id => hubProxy.server.getUpdateForOrder({ id, product, size }));

        }
    );
});