import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from 'src/app/models/Evento';
import { EventoService } from 'src/app/services/evento.service';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent implements OnInit {
  modalRef: BsModalRef;
  public eventos: Evento[] = [];
  public eventsFiltred: Evento[] = [];

  public widthImg = 150;
  public marginImg = 2;
  public showImage = true;
  private filterListed = '';

  public get filterList(): string{
    return this.filterListed;
  }

  public set filterList(value: string){
    this.filterListed = value;
    this.eventsFiltred = this.filterList ? this.filterEvents(this.filterList) : this.eventos;
  }

  public filterEvents(filterFor: string): Evento[]{
    filterFor = filterFor.toLocaleLowerCase();
    return this.eventos.filter(evento=> evento.tema.toLocaleLowerCase().indexOf(filterFor) !== -1 ||
    evento.local.toLocaleLowerCase().indexOf(filterFor) !== -1
    );
  }

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
    ) { }
  public ngOnInit(): void {
    this.spinner.show();
    this.getEventos();
  }

  public getEventos(): void {
    this.eventoService.getEventos().subscribe({
      next:(eventosResponse: Evento[]) => {
        this.eventos = eventosResponse;
        this.eventsFiltred = eventosResponse;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao Carregar os Eventos', 'Erro!');
      },
      complete: () => this.spinner.hide()
    });
  }

  openModal(template: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.toastr.success('O Evento foi excluído com sucesso', 'Excluído');
    this.modalRef.hide();
  }

  decline(): void {
    this.modalRef.hide();
  }

  detalheEvento(id: number): void{
    this.router.navigate([`eventos/detalhe/${id}`]);
  }
}
