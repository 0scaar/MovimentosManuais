import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MovimentoManualPage } from './movimento-manual-page';

describe('MovimentoManualPage', () => {
  let component: MovimentoManualPage;
  let fixture: ComponentFixture<MovimentoManualPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MovimentoManualPage],
    }).compileComponents();

    fixture = TestBed.createComponent(MovimentoManualPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
