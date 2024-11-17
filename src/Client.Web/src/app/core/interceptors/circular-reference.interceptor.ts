import { HttpInterceptorFn, HttpResponse } from '@angular/common/http';
import { map } from 'rxjs/operators';

const removeCircularReferences = (obj: any): any => {
    const seen = new WeakSet();

    return JSON.parse(JSON.stringify(obj, (key, value) => {
        if (key === '$id' || key === '$ref') {
            return undefined;
        }
        if (typeof value === 'object' && value !== null) {
            if (seen.has(value)) {
                return undefined;
            }
            seen.add(value);
        }
        return value;
    }));
};

export const circularReferenceInterceptor: HttpInterceptorFn = (request, next) => {
    return next(request).pipe(
        map(event => {
            if (event instanceof HttpResponse) {
                return event.clone({
                    body: removeCircularReferences(event.body)
                });
            }
            return event;
        })
    );
};
