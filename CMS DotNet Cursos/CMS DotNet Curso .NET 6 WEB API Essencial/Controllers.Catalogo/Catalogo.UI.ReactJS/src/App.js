import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';

import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Footer from './Components/Footer.js';
import logoCadastro from './assets/cadastro.png';

function App() {

  const baseUrl = "https://localhost:7176/api/v1/alunos";
  const [data, setData] = useState([]);
  const [updateData, setUpdateData] = useState(true);
  const [modalIncluir, setModalIncluir] = useState(false);
  const [modalEditar, setModalEditar] = useState(false);
  const [modalExcluir, setModalExcluir] = useState(false);
  const [alunoSelecionado, setAlunoSelecionado] = useState({ Id: '', Nome: '', Email: '', Idade: '' })

  const selecionarAluno = (aluno, opcao) => {
    setAlunoSelecionado(aluno);
    (opcao === "Editar") ? abrirFecharModalEditar() : abrirFecharModalExcluir();
  }

  const abrirFecharModalIncluir = () => {
    setModalIncluir(!modalIncluir);
  }

  const abrirFecharModalEditar = () => {
    setModalEditar(!modalEditar);
  }

  const abrirFecharModalExcluir = () => {
    setModalExcluir(!modalExcluir);
  }

  const handleChange = e => {
    const { name, value } = e.target;
    setAlunoSelecionado({
      ...alunoSelecionado, [name]: value
    });
    console.log(alunoSelecionado);
  }

  const pedidoGet = async () => {
    await axios.get(baseUrl)
      .then(response => {
        // console.log(response.data);
        setData(response.data);
      }).catch(error => {
        console.log(error);
      })
  }

  const pedidoPost = async () => {
    delete alunoSelecionado.Id;
    alunoSelecionado.idade = parseInt(alunoSelecionado.Idade);
    await axios.post(baseUrl, alunoSelecionado)
      .then(response => {
        // console.log(response.data);
        setData(data.concat(response.data));
        setUpdateData(true);
        abrirFecharModalIncluir();
      }).catch(error => {
        console.log(error);
      })
  }

  const pedidoPut = async () => {
    alunoSelecionado.Idade = parseInt(alunoSelecionado.Idade);
    await axios.put(baseUrl + "/" + alunoSelecionado.Id, alunoSelecionado)
      .then(response => {
        // console.log(response.data);
        var resposta = response.data;
        var dadosAuxiliar = data;

        // dadosAuxiliar.map(aluno => {
        //   if (aluno.Id === alunoSelecionado.Id) {
        //     aluno.Nome = resposta.Nome;
        //     aluno.Email = resposta.Email;
        //     aluno.Idade = resposta.Idade;
        //   }
        // });

        dadosAuxiliar.forEach(aluno => {
          if (aluno.Id === alunoSelecionado.Id) {
            aluno.Nome = resposta.Nome;
            aluno.Email = resposta.Email;
            aluno.Idade = resposta.Idade;
          }
        });

        setUpdateData(true);
        abrirFecharModalEditar();
      }).catch(error => {
        console.log(error);
      })
  }

  const pedidoDelete = async () => {
    await axios.delete(baseUrl + "/" + alunoSelecionado.Id)
      .then(response => {
        // console.log(response.data);
        setData(data.filter(aluno => aluno.Id !== response.data));
        setUpdateData(true);
        abrirFecharModalExcluir();
      }).catch(error => {
        console.log(error);
      })
  }

  useEffect(() => {
    if (updateData) {
      pedidoGet();
      setUpdateData(false);
    }
  }, [updateData])

  return (

    <div className="aluno-container">
      <br />
      <h3>Cadastro de Alunos</h3>

      <header>
        <img src={logoCadastro} alt='Cadastro' />
        <button className="btn btn-success" onClick={() => abrirFecharModalIncluir()}>Incluir Novo Aluno</button>
      </header>

      <table className="table table-bordered">
        <thead>
          <tr>
            <th>Id</th>
            <th>Nome</th>
            <th>Email</th>
            <th>Idade</th>
            <th>Operação</th>
          </tr>
        </thead>
        <tbody>
          {data.map(aluno => (
            <tr key={aluno.Id}>
              <td>{aluno.Id}</td>
              <td>{aluno.Nome}</td>
              <td>{aluno.Email}</td>
              <td>{aluno.Idade}</td>
              <td>
                <button className="btn btn-primary" onClick={() => selecionarAluno(aluno, "Editar")}>Editar</button> {"  "}
                <button className="btn btn-danger" onClick={() => selecionarAluno(aluno, "Excluir")}>Excluir</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      <Footer />

      <Modal isOpen={modalIncluir}>
        <ModalHeader>Incluir Alunos</ModalHeader>
        <ModalBody>
          <div className="form-group">
            <label>Nome: </label> <br />
            <input type="text" className="form-control" name="Nome" onChange={handleChange} /> <br />
            <label>Email: </label> <br />
            <input type="text" className="form-control" name="Email" onChange={handleChange} /> <br />
            <label>Idade: </label> <br />
            <input type="text" className="form-control" name="Idade" onChange={handleChange} /> <br />
          </div>
        </ModalBody>
        <ModalFooter>
          <button className="btn btn-primary" onClick={() => pedidoPost()}>Incluir</button>{"   "}
          <button className="btn btn-danger" onClick={() => abrirFecharModalIncluir()} >Cancelar</button>
        </ModalFooter>
      </Modal>

      <Modal isOpen={modalEditar}>
        <ModalHeader>Editar Aluno</ModalHeader>
        <ModalBody>
          <div className="form-group">
            <label>ID: </label>
            <input type="text" className="form-control" readOnly value={alunoSelecionado && alunoSelecionado.Id} />
            <br />
            <label>Nome: </label><br />
            <input type="text" className="form-control" name="Nome" onChange={handleChange} value={alunoSelecionado && alunoSelecionado.Nome} /><br />
            <label>Email: </label><br />
            <input type="text" className="form-control" name="Email" onChange={handleChange} value={alunoSelecionado && alunoSelecionado.Email} /><br />
            <label>Idade: </label><br />
            <input type="text" className="form-control" name="Idade" onChange={handleChange} value={alunoSelecionado && alunoSelecionado.Idade} /><br />
          </div>
        </ModalBody>
        <ModalFooter>
          <button className="btn btn-primary" onClick={() => pedidoPut()}>Editar</button>{"  "}
          <button className="btn btn-danger" onClick={() => abrirFecharModalEditar()} >Cancelar</button>
        </ModalFooter>
      </Modal>

      <Modal isOpen={modalExcluir}>
        <ModalBody>
          Confirma a exclusão deste(a) aluno(a) : {alunoSelecionado && alunoSelecionado.nome} ?
        </ModalBody>
        <ModalFooter>
          <button className="btn btn-danger" onClick={() => pedidoDelete()} > Sim </button>
          <button className="btn btn-secondary" onClick={() => abrirFecharModalExcluir()}> Não </button>
        </ModalFooter>
      </Modal>

    </div>

  );
}

export default App;
