// generate_slack_payload.js
const fs = require('fs');

function main(reportPath, outputPath) {
  const report = JSON.parse(fs.readFileSync(reportPath));
  const alerts = report.site[0].alerts;

  if (alerts && alerts.length > 0) {
    const blocks = alerts.map((alert, index) => ({
      type: 'section',
      text: {
        type: 'mrkdwn',
        text: `*${index + 1}. ${alert.alert}*\nRisk: ${alert.riskdesc}\nDescription: ${alert.desc}`
      }
    }));

    const payload = {
      text: "ZAP Scan Alerts:",
      blocks
    };

    fs.writeFileSync(outputPath, JSON.stringify(payload, null, 2));
    console.log("Slack payload saved to:", outputPath);
  } else {
    console.log("No alerts found in ZAP Report. No message saved.");
  }
}

const reportPath = process.argv[2];
const outputPath = process.argv[3];
main(reportPath, outputPath);
