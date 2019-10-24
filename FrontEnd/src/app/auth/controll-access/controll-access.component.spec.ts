import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ControllAccessComponent } from './controll-access.component';

describe('ControllAccessComponent', () => {
  let component: ControllAccessComponent;
  let fixture: ComponentFixture<ControllAccessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ControllAccessComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ControllAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
