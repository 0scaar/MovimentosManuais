const { spawn } = require('node:child_process');
const net = require('node:net');

const host = '127.0.0.1';
const requestedPort = Number.parseInt(process.env.PORT ?? '', 10);
const fallbackPort = Number.isNaN(requestedPort) ? 4200 : requestedPort;

function isAvailable(port) {
  return new Promise(resolve => {
    const server = net.createServer();

    server.once('error', () => resolve(false));
    server.once('listening', () => {
      server.close(() => resolve(true));
    });

    server.listen(port, host);
  });
}

async function findPort(startPort) {
  if (!Number.isNaN(requestedPort)) {
    return requestedPort;
  }

  for (let port = startPort; port < startPort + 100; port += 1) {
    if (await isAvailable(port)) {
      return port;
    }
  }

  throw new Error(`No available port found between ${startPort} and ${startPort + 99}.`);
}

async function main() {
  const port = await findPort(fallbackPort);
  const command = 'npx';
  const args = ['ng', 'serve', `--host=${host}`, `--port=${port}`];

  console.log(`Starting Angular dev server on http://${host}:${port}`);

  const child = spawn(command, args, {
    stdio: 'inherit',
    shell: true
  });

  child.on('error', error => {
    console.error(error);
    process.exit(1);
  });

  child.on('exit', code => process.exit(code ?? 0));
}

main().catch(error => {
  console.error(error);
  process.exit(1);
});
