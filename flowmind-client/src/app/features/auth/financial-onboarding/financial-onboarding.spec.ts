import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FinancialOnboarding } from './financial-onboarding';

describe('FinancialOnboarding', () => {
  let component: FinancialOnboarding;
  let fixture: ComponentFixture<FinancialOnboarding>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FinancialOnboarding],
    }).compileComponents();

    fixture = TestBed.createComponent(FinancialOnboarding);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
