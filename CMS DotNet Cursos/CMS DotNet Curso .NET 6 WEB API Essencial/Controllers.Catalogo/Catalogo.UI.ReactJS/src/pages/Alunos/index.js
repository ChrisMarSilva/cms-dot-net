import React, { useState, useEffect } from 'react';
import { Navigate, Link, useNavigate } from 'react-router-dom'; // useHistory
import { FiXCircle, FiEdit, FiUserX } from 'react-icons/fi';
import './styles.css';
import api from '../../services/api';
import logoCadastro from '../../assets/cadastro1.png';

export default function Alunos() {

    //const [authenticated, setauthenticated] = useState(null);
    //const [authenticated, setauthenticated] = useState(localStorage.getItem("authenticated") || false);
    const [searchInput, setSearchInput] = useState('');
    const [filtro, setFiltro] = useState([]);
    const [alunos, setAlunos] = useState([]);
    const email = localStorage.getItem('email');
    const token = localStorage.getItem('token');
    // const history = useHistory();
    const navigate = useNavigate();
    const authorization = { headers: { Authorization: `Bearer ${token}` } }

    const searchAlunos = (searchValue) => {
        setSearchInput(searchValue);
        // console.log('searchAlunos - searchValue: "' + searchValue + '" - searchInput: "' + searchInput + '"');
        // if (searchInput !== '') {
        //     const dadosFiltrados = alunos.filter((item) => {
        //         return Object.values(item).join('').toLowerCase().includes(searchInput.toLowerCase())
        //     });
        //     setFiltro(dadosFiltrados);
        // }
        // else {
        //     setFiltro(alunos);
        // }
    }

    // useEffect(() => {
    //     const loggedInUser = localStorage.getItem("authenticated") || false;
    //     if (loggedInUser) {
    //         setauthenticated(loggedInUser);
    //     }
    // }, [])

    useEffect(() => {
        //if (authenticated) {
        const url = 'api/v1/alunos'; // 'api/alunos'
        api.get(url, authorization).then(response => {
            setAlunos(response.data);
        }, token)
        //}
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])

    useEffect(() => {
        // console.log('useEffect - searchInput.value: "' + searchInput + '" searchInput.length: ' + searchInput.length);
        if (searchInput !== '') {
            const dadosFiltrados = alunos.filter((item) => {

                // return Object.values(item).join('').toLowerCase().includes(searchInput.toLowerCase());

                // return item.nome.toString().toLowerCase().indexOf(searchInput.toLowerCase()) > -1
                // || item.email.toString().toLowerCase().indexOf(searchInput.toLowerCase()) > -1
                // || item.idade.toString().toLowerCase().indexOf(searchInput.toLowerCase()) > -1;

                // return item.nome.toString().toLowerCase().split(" ").indexOf(searchInput.toLowerCase()) !== -1
                // || item.email.toString().toLowerCase().split(" ").indexOf(searchInput.toLowerCase()) !== -1
                // || item.idade.toString().toLowerCase().split(" ").indexOf(searchInput.toLowerCase()) !== -1

                return item.nome.toString().toLowerCase().includes(searchInput.toLowerCase())
                    || item.email.toString().toLowerCase().includes(searchInput.toLowerCase())
                    || item.idade.toString().toLowerCase().includes(searchInput.toLowerCase());
            });
            setFiltro(dadosFiltrados);
        }
        else {
            setFiltro(alunos);
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [searchInput])

    async function logout() {
        try {

            localStorage.clear();
            // sessionStorage.clear();

            localStorage.setItem('token', '');
            localStorage.setItem("authenticated", false);
            authorization.headers = '';

            //history.push('/');
            navigate('/');
        } catch (err) {
            alert('Não foi possível fazer o logout' + err);
        }
    }

    async function editAluno(id) {
        try {
            //history.push(`alunos/novo/${id}`);
            navigate(`novo/${id}`);
        } catch (error) {
            alert('Não foi possível editar o aluno')
        }
    }

    async function deleteAluno(id) {
        try {
            if (window.confirm('Deseja deletar o aluno de id = ' + id + ' ?')) {
                const url = `api/v1/alunos/${id}`; // `api/alunos/${id}`
                await api.delete(url, authorization);
                setAlunos(alunos.filter(aluno => aluno.id !== id));
            }
        } catch (error) {
            alert('Não foi possível excluir o aluno')
        }
    }

    // if (authenticated === false) {
    //     return <Navigate to="/" replace />;
    //     //     return <h1>aaa: {authenticated}</h1>;
    //     //    return <Redirect to="/" />
    // }

    return (

        <div className="aluno-container">

            <header>
                <img src={logoCadastro} alt="Cadastro" />
                <span>Bem-Vindo, <strong>{email}</strong>!</span>
                <Link className="button" to="novo/0">Novo Aluno</Link>
                <button onClick={logout} type="button"> <FiXCircle size={35} color="#17202a" /> </button>
            </header>

            <form>
                <input type='text' placeholder='Filtrar por nome...' onChange={(e) => searchAlunos(e.target.value)} />
            </form>

            <h1>Relação de Alunos</h1>

            {
                searchInput.length >= 1 ?
                    (
                        <ul>
                            {filtro.map(aluno => (
                                <li key={aluno.id}>
                                    <b>Nome:</b>{aluno.nome}<br /><br />
                                    <b>Email:</b>{aluno.email}<br /><br />
                                    <b>Idade:</b>{aluno.idade}<br /><br />
                                    <button onClick={() => editAluno(aluno.id)} type="button"> <FiEdit size="25" color="#17202a" /> </button>
                                    <button type="button" onClick={() => deleteAluno(aluno.id)}> <FiUserX size="25" color="#17202a" /> </button>
                                </li>
                            ))}
                        </ul>
                    )
                    :
                    (
                        <ul>
                            {alunos.map(aluno => (
                                <li key={aluno.id}>
                                    <b>Nome:</b>{aluno.nome}<br /><br />
                                    <b>Email:</b>{aluno.email}<br /><br />
                                    <b>Idade:</b>{aluno.idade}<br /><br />
                                    <button onClick={() => editAluno(aluno.id)} type="button"> <FiEdit size="25" color="#17202a" /> </button>
                                    <button type="button" onClick={() => deleteAluno(aluno.id)}> <FiUserX size="25" color="#17202a" /> </button>
                                </li>
                            ))}
                        </ul>
                    )
            }
        </div>
    );
}