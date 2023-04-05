import { Component, OnInit } from '@angular/core';
import { Categoria } from 'src/model/categoria';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from 'src/services/api.service';

@Component({
  selector: 'app-categoria-detalhe',
  templateUrl: './categoria-detalhe.component.html',
  styleUrls: ['./categoria-detalhe.component.css']
})
export class CategoriaDetalheComponent implements OnInit {
  categoria: Categoria = new Categoria('', '', ''); // = { id: '', nome: '', imagemUrl: '' };
  isLoadingResults = true;
  constructor(private router: Router, private route: ActivatedRoute, private api: ApiService) { }

  public ngOnInit() {
    this.getCategoria(this.route.snapshot.params['id']);
  }

  public getCategoria(id) {
    this.api.getCategoria(id)
      .subscribe((res: any) => {
        console.log(res);
        this.categoria.id = res.Id;
        this.categoria.nome = res.Nome;
        this.categoria.imagemUrl = res.ImagemUrl;
        this.isLoadingResults = false;
      });
  }

  public deleteCategoria(id) {
    this.isLoadingResults = true;
    this.api.deleteCategoria(id)
      .subscribe((res: any) => {
        this.isLoadingResults = false;
        this.router.navigate(['/categorias']);
      }, (err) => {
        console.log(err);
        this.isLoadingResults = false;
      }
      );
  }
}
