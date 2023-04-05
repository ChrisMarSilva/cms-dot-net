import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, NgForm, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiService } from 'src/services/api.service';

@Component({
  selector: 'app-categoria-editar',
  templateUrl: './categoria-editar.component.html',
  styleUrls: ['./categoria-editar.component.css']
})
export class CategoriaEditarComponent implements OnInit {

  categoriaId: String = '';
  categoriaForm: FormGroup;
  nome: String = '';
  imagemUrl: String = '';

  isLoadingResults = false;
  constructor(private router: Router, private route: ActivatedRoute,
    private api: ApiService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.getCategoria(this.route.snapshot.params['id']);
    this.categoriaForm = this.formBuilder.group({
      'categoriaId': [null],
      'nome': [null, Validators.required],
      'imagemUrl': [null, Validators.required]
    });
  }

  getCategoria(id) {
    this.api.getCategoria(id)
      .subscribe((res: any) => {
        this.categoriaId = res.Id;
        this.categoriaForm.setValue({
          categoriaId: res.Id,
          nome: res.Nome,
          imagemUrl: res.ImagemUrl,
        });
      });
  }

  updateCategoria(form: NgForm) {
    this.isLoadingResults = true;
    this.api.updateCategoria(this.categoriaId, form)
      .subscribe((res: any) => {
        this.isLoadingResults = false;
        this.router.navigate(['/categoria-detalhe/' + this.categoriaId]);
      }, (err) => {
        console.log(err);
        this.isLoadingResults = false;
      }
      );
  }
}
