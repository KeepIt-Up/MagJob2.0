import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ContainerComponent } from './container.component';
import { BehaviorSubject, Subject } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

describe('ContainerComponent', () => {
    let component: ContainerComponent<any>;
    let fixture: ComponentFixture<ContainerComponent<any>>;
    let testSource$: BehaviorSubject<any>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [ContainerComponent]
        }).compileComponents();

        testSource$ = new BehaviorSubject<any>(null);
        fixture = TestBed.createComponent(ContainerComponent);
        component = fixture.componentInstance;
        component.source$ = testSource$;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should initialize with loading state', () => {
        const freshComponent = new ContainerComponent<any>();

        expect(freshComponent._state.isLoading).toBe(true);
        expect(freshComponent._state.data).toBeUndefined();
        expect(freshComponent._state.error).toBeUndefined();
    });

    it('should emit state when data is received', () => {
        const testData = { test: 'data' };
        const stateSpy = spyOn(component.onStateChange, 'emit');

        testSource$.next(testData);

        expect(stateSpy).toHaveBeenCalledWith({
            isLoading: false,
            data: testData
        });
        expect(component._state.isLoading).toBeFalsy();
        expect(component._state.data).toEqual(testData);
        expect(component._state.error).toBeUndefined();
    });

    it('should emit state when error occurs', () => {
        const testError = new HttpErrorResponse({ status: 404, statusText: 'Not Found' });
        const stateSpy = spyOn(component.onStateChange, 'emit');

        testSource$.error(testError);

        expect(stateSpy).toHaveBeenCalledWith({
            isLoading: false,
            error: testError
        });
        expect(component._state.isLoading).toBeFalsy();
        expect(component._state.data).toBeUndefined();
        expect(component._state.error).toEqual(testError);
    });

    it('should resubscribe when source$ input changes', () => {
        const newSource$ = new BehaviorSubject<any>({ newData: 'test' });
        const stateSpy = spyOn(component.onStateChange, 'emit');

        // Simulate input change
        component.source$ = newSource$;
        component.ngOnChanges({
            source$: {
                currentValue: newSource$,
                previousValue: testSource$,
                firstChange: false,
                isFirstChange: () => false
            }
        });

        expect(stateSpy).toHaveBeenCalledWith({
            isLoading: false,
            data: { newData: 'test' }
        });
    });

    it('should cleanup subscription on destroy', () => {
        const testSubject = new Subject<void>();
        const nextSpy = spyOn(testSubject, 'next');
        const completeSpy = spyOn(testSubject, 'complete');

        component['destroy$'] = testSubject;
        component.ngOnDestroy();

        expect(nextSpy).toHaveBeenCalled();
        expect(completeSpy).toHaveBeenCalled();
    });
}); 