import { FormGroup } from '@angular/forms';

export function mapApiErrorsToForm(resposta: any, form: FormGroup): void {
  if (!resposta) return;

  if (resposta.errors) {
    for (const campo in resposta.errors) {
      const mensagens = resposta.errors[campo];

      if (form.get(campo)) {
        form.get(campo)?.setErrors({
          api: mensagens.join(', ')
        });
      } else {
        form.setErrors({
          ...(form.errors || {}),
          [campo]: mensagens.join(', ')
        });
      }
    }
  }

  else if (resposta.error && typeof resposta.error === 'string') {
    form.setErrors({
      ...(form.errors || {}),
      api: resposta.error
    });
  }
}
