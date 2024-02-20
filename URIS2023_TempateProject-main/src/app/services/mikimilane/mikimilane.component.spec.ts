import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MikimilaneComponent } from './mikimilane.component';

describe('MikimilaneComponent', () => {
  let component: MikimilaneComponent;
  let fixture: ComponentFixture<MikimilaneComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MikimilaneComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MikimilaneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
