// import { Button, Image } from "react-bootstrap";
// import { Aluno } from "../../models/AlunoModel";

// import { Container, Content } from "./styles";

// interface Props {
//     aluno: Aluno;
//     onClickEdit: () => void;
// }
// export function AlunoCard({ aluno, onClickEdit }: Props) {
//     return (
//         <div>
//             <Container>
//                 <Image roundedCircle src={aluno.email} />
//                 <Content>
//                     <h3>{aluno.name}</h3>
//                     <Button onClick={onClickEdit} style={{ width: "100px" }} variant="primary">
//                         editar
//                     </Button>
//                 </Content>
//             </Container>
//         </div>
//     );
// }

import React, { Component } from 'react';

class AlunoCard extends Component {

    constructor(props) {
        super(props);
        this.state = {};
    }

    render() {
        return (

            <div>

            </div>

        );
    }
}

export default AlunoCard;