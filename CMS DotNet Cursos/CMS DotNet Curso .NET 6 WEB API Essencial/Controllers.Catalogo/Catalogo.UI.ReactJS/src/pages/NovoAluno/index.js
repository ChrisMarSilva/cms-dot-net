import React, { useEffect, useState } from 'react';
import { Link, useParams, useNavigate } from 'react-router-dom'; // useHistory
import { FiCornerDownLeft, FiUserPlus } from 'react-icons/fi';
import './styles.css';
import api from '../../services/api';

export default function NovoAluno() {

    const [id, setId] = useState(null);
    const [nome, setNome] = useState('');
    const [email, setEmail] = useState('');
    const [idade, setIdade] = useState(0);
    const { alunoId } = useParams();
    // const history = useHistory();
    const navigate = useNavigate();
    const token = localStorage.getItem('token');
    const authorization = { headers: { Authorization: `Bearer ${token}` } }

    useEffect(() => {
        //console.log('useEffect: ' + alunoId);
        if (alunoId === '0')
            return;
        else
            loadAluno();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [alunoId])

    async function loadAluno() {
        try {
            console.log('loadAluno');

            const url = `api/v1/alunos/${alunoId}`; // `api/alunos/${alunoId}`
            const response = await api.get(url, authorization);

            setId(response.data.id);
            setNome(response.data.nome);
            setEmail(response.data.email);
            setIdade(response.data.idade);

        } catch (error) {
            alert('Erro ao recuperar o aluno ' + error);
            //history.push('/alunos');
            navigate('/alunos');
        }
    }

    async function saveOrUpdate(event) {
        try {
            event.preventDefault();

            const data = { nome, email, idade }

            if (alunoId === '0') {
                const url = 'api/v1/alunos'; // 'api/alunos'
                await api.post(url, data, authorization);
            }
            else {
                data.id = id;
                const url = `api/v1/alunos/${id}`; // `api/alunos/${id}`
                await api.put(url, data, authorization)
            }

        } catch (error) {
            alert('Erro ao gravar aluno ' + error);
        }
        //history.push('/alunos');
        navigate('/alunos');
    }

    return (
        <div className="novo-aluno-container">
            <div className="content">
                <section className="form"><FiUserPlus size="105" color="#17202a" />
                    <h1>{alunoId === '0' ? 'Incluir Novo Aluno' : 'Atualizar Aluno'}</h1>
                    <Link className="back-link" to="/alunos">
                        <FiCornerDownLeft size="25" color="#17202a" /> Retornar
                    </Link>
                </section>
                <form onSubmit={saveOrUpdate}>
                    <input placeholder="Nome" value={nome} onChange={e => setNome(e.target.value)} />
                    <input placeholder="Email" value={email} onChange={e => setEmail(e.target.value)} />
                    <input placeholder="Idade" value={idade} onChange={e => setIdade(e.target.value)} />
                    <button className="button" type="submit">{alunoId === '0' ? 'Incluir ' : 'Atualizar '}</button>
                </form>
            </div>
        </div>
    );
}
