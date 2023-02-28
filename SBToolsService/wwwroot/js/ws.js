class SmallBusinessInfo {
    businessName = "";
    sales = 0;
    ownerSalary = 0;
    depreciation = 0;
    interest = 0;
    ownerPersonalExpenses = 0;
    utilities = 0;
    rent = 0;
    payroll = 0;
    miscExpensese = 0;
    sdeMultiple = 0;
    sellableInventory = 0;
    askingPrice = 0;
}

class WSHelper {

    // Example POST method implementation:
    static async postData(url = "", data) {

        var bdy = JSON.stringify(data);

        // Default options are marked with *
        const response = await fetch(url, {
            method: "PUT", // *GET, POST, PUT, DELETE, etc.
            mode: "cors", // no-cors, *cors, same-origin
            cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
            credentials: "same-origin", // include, *same-origin, omit
            headers: {
                'Access-Control-Allow-Origin': '*',
                "Content-Type": "application/json",
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            redirect: "follow", // manual, *follow, error
            referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
            body: bdy, // body data type must match "Content-Type" header
        });
        return response.json(); // parses JSON response into native JavaScript objects
    }

    static async submitSmallBusinessInfo(smallBusinessInfo) {

        //this.postData("https://localhost:44366/SmallBusiness", smallBusinessInfo)
        //    .then((data) => {
        //        console.log(data); // JSON data parsed by `data.json()` call
        //        return data;
        //    });

        const result = await this.postData("https://localhost:44366/SmallBusiness", smallBusinessInfo)

        return result
    }
}

export { SmallBusinessInfo, WSHelper }