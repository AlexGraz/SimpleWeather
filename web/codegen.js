const fs = require("fs");
const path = require("path");
const axios = require("axios");
const https = require("https");

const httpsAgent = new https.Agent({
  rejectUnauthorized: false,
});

async function downloadCodeGen(apiPath, localFilePath) {
  let response = await axios.get(
    "https://localhost:7179/api/v1/code-gen/" + apiPath,
    { responseType: "stream", httpsAgent }
  );

  if (!fs.existsSync(path.dirname(localFilePath))) {
    fs.mkdirSync(path.dirname(localFilePath));
  }

  const writer = fs.createWriteStream(localFilePath);
  response.data.pipe(writer);
}

void downloadCodeGen("api-definitions.ts", "src/code-gen/api-definitions.ts");
