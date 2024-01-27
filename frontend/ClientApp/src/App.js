import { useEffect, useState } from "react";
import "bootstrap/dist/css/bootstrap.min.css"
import { FaEdit } from "react-icons/fa"
import { MdDelete } from "react-icons/md";
import { RiArchiveDrawerFill } from "react-icons/ri";
import { TiDeleteOutline } from "react-icons/ti";
import { Dropdown, DropdownItem, DropdownMenu, DropdownToggle, Input, FormGroup, Label } from 'reactstrap'






const App = () => {

    
    //Estados
    const [loading, setLoading] = useState(false);
    const [notas, setNotas] = useState([]);
    const [descripcion, setDescripcion] = useState("");
    const [notaEditando, setNotaEditando] = useState("");
    const [dropdown, setDropdown] = useState(false);
    const [filtroEstado, setFiltroEstado] = useState("Ambos");
    const [categoria, setCategoria] = useState([]);
    const [notasCategoria, setNotasCategoria] = useState([])
    const [categoriasSeleccionadas, setCategoriasSeleccionadas] = useState([]);
    const [filtroCategoria, setFiltroCategoria] = useState("Todas");
    const [dropdown2, setDropdown2] = useState(false);
    const [dropdown3, setDropdown3] = useState(false);


    const abrirCerrarDropdown = () => {
        setDropdown(!dropdown);
    }
    const abrirCerrarDropdown2 = () => {
        setDropdown2(!dropdown2);
    }
    const abrirCerrarDropdown3 = () => {
        setDropdown3(!dropdown3);
    }

    const cambiarFiltroEstado = (estadoSeleccionado) => {
        setFiltroEstado(estadoSeleccionado);
        mostrarNotas(); 
    };

    const mostrarNotas = async () => {
        try {
            setLoading(true);

            const response = await fetch("api/notas/Lista");

            if (response.ok) {
                const data = await response.json();

                let notasFiltradas = [...data];

                if (filtroEstado === "Archivadas") {
                    notasFiltradas = notasFiltradas.filter(nota => nota.archivada);
                } else if (filtroEstado === "No Archivadas") {
                    notasFiltradas = notasFiltradas.filter(nota => !nota.archivada);
                }

                if (filtroCategoria !== "Todas") {
                    notasFiltradas = notasFiltradas.filter(nota =>
                        nota.categorias.includes(filtroCategoria)
                    );
                }

                setNotas(notasFiltradas);
            } else {
                console.log("Status Code" + response.status);
            }
        } catch (error) {
            console.error("Error al cargar notas:", error);
        } finally {
            setLoading(false);
        }
    };

    const mostrarCategorias = async () => {
        
            const response = await fetch("api/categoria/Lista");
            if (response.ok) {
                const data = await response.json();
                setCategoria(data)
            }
            else {
                console.log("Status Code" + response.status)
            }
    }

    const mostrarNotasCategoria = async () => {

        const response = await fetch("api/notascategoria/Lista")
        if (response.ok) {
            const data = await response.json()
            setNotasCategoria(data)

        }
        else {
            console.log("Status Code" + response.status)
        }
    }

    const handleCategoriaSeleccionada = (categoria) => {
        setFiltroCategoria(categoria);
        mostrarNotas();
    };
    

    useEffect(() => {
        mostrarNotas();
        mostrarCategorias();
        mostrarNotasCategoria();
    }, [filtroEstado, filtroCategoria]);

  

    const archivarDesarchivar = async (idNota, archivadaActual) => {
        try {
            const response = await fetch(`api/notas/ModificarArchivada/${idNota}`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json;charset=utf-8",
                },
                body: JSON.stringify({ archivada: archivadaActual }),
            });

            if (response.ok) {
                await mostrarNotas();
            }
        } catch (error) {
            console.error("Error al archivar/desarchivar nota:", error);
        }
    };


    const eliminarNota = async (id) => {

        const response = await fetch(`api/notas/Cerrar/${id}`, {
            method: "DELETE"

        })
        if (response.ok) {

            await mostrarNotas();
        }
    }
    const eliminarCategoriasDeNota = async (idNota) => {
        try {
            const response = await fetch(`api/notas/EliminarCategorias/${idNota}`, {
                method: "DELETE"
            });

            if (response.ok) {
                console.log("Categorías eliminadas con éxito");
           
                mostrarNotas();
            } else {
                console.log("Error al eliminar categorías. Estado: " + response.status);
            }
        } catch (error) {
            console.error("Error al enviar la solicitud:", error);
        }
    };

    const editarNota = (id, descripcion) => {
        setNotaEditando({ id, descripcion });
        setDescripcion(descripcion);
    };
   
    const cancelarEdicion = () => {
        setNotaEditando(null);
        setDescripcion("");
    };
    const modificarNota = async () => {
        if (!notaEditando) {
            return;
        }

        const { id, descripcion: descripcionEditando, IdCategorias } = notaEditando;

        const response = await fetch(`api/notas/Modificar/${id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json;charset=utf-8",
            },
            body: JSON.stringify({
                descripcion: descripcion,
                IdCategorias: Object.keys(categoriasSeleccionadas).filter(key => categoriasSeleccionadas[key])
            }),
        });

        if (response.ok) {
            setDescripcion("");
            setNotaEditando(null);
            await mostrarNotas();
        }
    };

    const guardarNotaConCategoria = async () => {
        try {
            const response = await fetch("api/notas/GuardarConCategoria", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json;charset=utf-8",
                },
                body: JSON.stringify({
                    descripcion: descripcion,
                    archivada: false,
                    IdCategorias: Object.keys(categoriasSeleccionadas).filter(key => categoriasSeleccionadas[key]),
                }),
            });

            if (response.ok) {
                setDescripcion("");
                setCategoriasSeleccionadas([]);
                await mostrarNotas();
            } else {
                console.log("Error al guardar la nota con categoria. Estado: " + response.status);
            }
        } catch (error) {
            console.error("Error al enviar la solicitud:", error);
        }
    };

    

    return (
        <div className="container bg-dark p-4 vh100">
            <h2 className="text-white">Notas</h2>

            <div className="row">

                <div className="col-sm-6">
                    <br></br>   
                    <Dropdown isOpen={dropdown} toggle={abrirCerrarDropdown}>
                        <h5 className="text-white"> Estado</h5>
                        <DropdownToggle caret>
                             {filtroEstado }
                        </DropdownToggle>
                        <DropdownMenu>
                            <DropdownItem onClick={() => cambiarFiltroEstado("Archivadas")}> Archivadas </DropdownItem>
                            <DropdownItem onClick={() => cambiarFiltroEstado("No Archivadas")}> No Archivadas </DropdownItem>
                            <DropdownItem onClick={() => cambiarFiltroEstado("Ambos")}> Ambos </DropdownItem>
                        </DropdownMenu>
                    </Dropdown>
                </div>
                
                <div className="col-sm-6">
                    <br></br>
                    <Dropdown isOpen={dropdown2} toggle={abrirCerrarDropdown2}>
                        <h5 className="text-white"> Categoria</h5>
                            <DropdownToggle caret>
                                {filtroCategoria}
                            </DropdownToggle>
                            <DropdownMenu>
                            {categoria.map((item) => (
                                <DropdownItem key={item.idCategoria} onClick={() => handleCategoriaSeleccionada(item.descripcion)}>
                                                                    
                                    <label htmlFor={item.idCategoria}>{item.descripcion}</label>
                                </DropdownItem>
                            ))}
                            </DropdownMenu>
                        </Dropdown>
                </div>

            </div>
            {loading && (
                <div className="text-white">
                    <h5 > Trayendo notas...</h5>
                </div>
            )}
            <div className="row mt-4">

                <form onSubmit={(e) => {
                    e.preventDefault();
                    notaEditando ? modificarNota() : guardarNotaConCategoria();
                }}>
                    <div className="input-group">
                        <input
                            type="text"
                            className="form-control"
                            placeholder="Escriba su nota"
                            value={descripcion}
                            onChange={(e) => setDescripcion(e.target.value)}
                        />
                        <Dropdown isOpen={dropdown3} toggle={abrirCerrarDropdown3}>
                            <DropdownToggle caret>
                                Categoria
                            </DropdownToggle>
                            <DropdownMenu>
                                {categoria.map((item) => (
                                    <DropdownItem key={item.idCategoria}>
                                        <FormGroup check>
                                            <Label check>
                                                <Input
                                                    type="checkbox"
                                                    id={item.idCategoria}
                                                    checked={categoriasSeleccionadas[item.idCategoria] || false}
                                                    onChange={() => {
                                                        setCategoriasSeleccionadas((prevState) => ({
                                                            ...prevState,
                                                            [item.idCategoria]: !prevState[item.idCategoria]
                                                        }));
                                                    }}
                                                />
                                                {item.descripcion}
                                            </Label>
                                        </FormGroup>
                                    </DropdownItem>
                                ))}
                            </DropdownMenu>
                        </Dropdown>
                        <div className="input-group-append">
                            
                            <button className="btn btn-success" type="submit">
                                {notaEditando ? "Guardar" : "Agregar"}
                            </button>
                            
                            {notaEditando && (
                                <button
                                    className="btn btn-outline-secondary"
                                    type="button"
                                    onClick={cancelarEdicion}
                                >
                                    Cancelar
                                </button>
                            )}

                        </div>
                        
                    </div>
                </form>
                <br/>
                <br/>
                <div className="col-sm-12">
                    <div className="list-group">
                        {
                            notas.map(
                                (item) => (
                                    <div key={item.idNota} className="list-group-item list-group-item-action">
                                        <div className="d-flex justify-content-between">
                                            <h5 className={item.archivada ? "text-secondary" : "text-primary"}>
                                                {item.descripcion}
                                            </h5>
                                                       <div key={item.idNotaCategoria} >
                                                            <div className="d-flex justify-content-between">
                                                                <h5 className="text-secondary">
                                                                                                         
                                                                </h5>
                                                    <h6 className="text-secondary">
                                                        {item.categorias ? (
                                                            <>
                                                                {item.categorias.join(' - ')}
                                                                {item.categorias.length > 0 && (
                                                                    <TiDeleteOutline onClick={() => eliminarCategoriasDeNota(item.idNota)} />
                                                                )}
                                                            </>
                                                        ) : null}
                                                    </h6>
                                                            </div>
                                                        </div> 
                                            <div className="d-flex justify-content-around">
                                                <button
                                                    className={`btn btn-sm ${item.archivada ? 'btn-outline-primary' : 'btn-outline-secondary'}`}
                                                    onClick={() => archivarDesarchivar(item.idNota, item.archivada)}
                                                >
                                                    <RiArchiveDrawerFill />
                                                </button>
                                                <button className="btn btn-sm btn-outline-secondary" onClick={() => editarNota(item.idNota, item.descripcion)}><FaEdit/></button>
                                                <button className="btn btn-sm btn-outline-danger" onClick={() => eliminarNota(item.idNota)}><MdDelete /></button>
                                            </div>
                                        </div>
                                     
                                    </div>
                                )
                            )
                        }
                    </div>
                </div>
            </div>
        </div>
    )
}

export default App;