import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {
  evento = {} as Evento;
  form: FormGroup;
  estadoSalvar = 'post';

  get f(): any{
    return this.form.controls;

  }

  get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: false
    };
  }

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private router: ActivatedRoute,
    private eventoService: EventoService,
    private spinner: NgxSpinnerService,
    private toaster: ToastrService
    ) {
    this.localeService.use('pt-br');
   }

   public carregarEvento(): void {
     const eventoIdParam = this.router.snapshot.paramMap.get('id');

     if (eventoIdParam !== null){
       this.spinner.show();

       this.estadoSalvar = 'put';

       this.eventoService.getEventoById(+eventoIdParam).subscribe(
        (evento: Evento) => {
          this.evento = {...evento};
          this.form.patchValue(this.evento);
        },
        (error: any) => { console.log(error); this.spinner.hide(); this.toaster.error('Erro ao tentar carregar evento!') },
        () => this.spinner.hide(),
       );
     }
   }

  ngOnInit(): void {
    this.carregarEvento();
    this.validation();
  }

  public validation(): void{
    this.form = this.fb.group(
      {
        tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)] ],
        local: ['', Validators.required],
        dataEvento: ['', Validators.required],
        qtdPessoa: ['', [Validators.required, Validators.max(120000)] ],
        telefone: ['', Validators.required],
        email: ['', [Validators.required, Validators.email] ],
        imagemUrl: ['', Validators.required],
      }
    );
  }

  public resetForm(): void{
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl):any{
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

  public salvarAlteracao(): void{
    this.spinner.show();
    if (this.form.valid){


      this.evento = (this.estadoSalvar === 'post')
        ? {...this.form.value}
        :  this.evento = {id: this.evento.id, ...this.form.value};

      this.eventoService[this.estadoSalvar](this.evento).subscribe(
        () => this.toaster.success('Evento salvo com sucesso!', 'Sucesso'),
        (error: any) => {
          console.error(error);
          this.spinner.hide();
          this.toaster.error('Evento não pode ser salvo', 'Erro');
        },
        () => this.spinner.hide()
      );
    }
  }

}
