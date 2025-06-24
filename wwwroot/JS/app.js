const apiUrl = "https://localhost:7274/api"; // Ajusta si usas otro puerto o ruta

function mostrarSeccion(id) {
    document.querySelectorAll(".seccion").forEach(s => s.style.display = "none");
    document.getElementById(id).style.display = "block";
}

window.onload = () => {
    mostrarSeccion("clientes");
    cargarClientes();
    cargarCuentas();
    cargarTransferencias();
};

// ========== CLIENTES ==========
async function cargarClientes() {
    const res = await fetch(`${apiUrl}/Clientes`);
    const data = await res.json();
    const tbody = document.getElementById("clienteTabla");
    tbody.innerHTML = "";
    data.forEach(c => {
        const fila = `<tr>
            <td>${c.cedula}</td>
            <td>${c.nombre}</td>
            <td>${c.apellido}</td>
            <td><button onclick="eliminarCliente('${c.cedula}')">Eliminar</button></td>
        </tr>`;
        tbody.innerHTML += fila;
    });
}

async function eliminarCliente(cedula) {
    await fetch(`${apiUrl}/Clientes/${cedula}`, { method: "DELETE" });
    cargarClientes();
}

document.getElementById("clienteForm").addEventListener("submit", async e => {
    e.preventDefault();
    const cedula = document.getElementById("dniCliente").value;
    const nombre = document.getElementById("nombreCliente").value;
    const apellido = document.getElementById("apellidoCliente").value;

    await fetch(`${apiUrl}/Clientes`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ cedula, nombre, apellido })
    });

    cargarClientes();
    e.target.reset();
});

// ========== CUENTAS ==========
async function cargarCuentas() {
    const res = await fetch(`${apiUrl}/Cuentas`);
    const data = await res.json();
    const tbody = document.getElementById("cuentaTabla");
    tbody.innerHTML = "";
    data.forEach(c => {
        const fila = `<tr>
            <td>${c.numero}</td>
            <td>${c.tipo}</td>
            <td>${c.saldo}</td>
            <td>${c.cedulaCliente}</td>
            <td><button onclick="eliminarCuenta('${c.numero}')">Eliminar</button></td>
        </tr>`;
        tbody.innerHTML += fila;
    });
}

async function eliminarCuenta(numero) {
    await fetch(`${apiUrl}/Cuentas/${numero}`, { method: "DELETE" });
    cargarCuentas();
}

document.getElementById("cuentaForm").addEventListener("submit", async e => {
    e.preventDefault();
    const numero = document.getElementById("numeroCuenta").value;
    const tipo = document.getElementById("tipoCuenta").value;
    const saldo = parseFloat(document.getElementById("saldoCuenta").value);
    const cedulaCliente = document.getElementById("clienteIdCuenta").value;

    await fetch(`${apiUrl}/Cuentas`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ numero, tipo, saldo, cedulaCliente })
    });

    cargarCuentas();
    e.target.reset();
});

// ========== TRANSFERENCIAS ==========
async function cargarTransferencias() {
    const res = await fetch(`${apiUrl}/Transferencias`);
    const data = await res.json();
    const tbody = document.getElementById("transferenciaTabla");
    tbody.innerHTML = "";
    data.forEach(t => {
        const fila = `<tr>
            <td>${t.cuentaOrigen}</td>
            <td>${t.cuentaDestino}</td>
            <td>${t.valor}</td>
            <td>${t.fecha}</td>
            <td><button onclick="eliminarTransferencia(${t.numero})">Eliminar</button></td>
        </tr>`;
        tbody.innerHTML += fila;
    });
}

async function eliminarTransferencia(numero) {
    await fetch(`${apiUrl}/Transferencias/${numero}`, { method: "DELETE" });
    cargarTransferencias();
}

document.getElementById("transferenciaForm").addEventListener("submit", async e => {
    e.preventDefault();
    const cuentaOrigen = document.getElementById("origen").value;
    const cuentaDestino = document.getElementById("destino").value;
    const valor = parseFloat(document.getElementById("valor").value);

    await fetch(`${apiUrl}/Transferencias`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ cuentaOrigen, cuentaDestino, valor, fecha: new Date().toISOString() })
    });

    cargarTransferencias();
    e.target.reset();
});
